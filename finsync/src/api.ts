export const base = String(import.meta.env.VITE_API_BASE_URL || '').replace(/\/$/,'')
type Operation = { tags?: string[]; summary?: string }
type Paths = Record<string, Record<string, Operation>>
let paths: Promise<Paths> | undefined

export async function request(path: string, options: RequestInit = {}, token?: string) {
 const headers = new Headers(options.headers)
 if (options.body && !(options.body instanceof FormData)) headers.set('Content-Type','application/json')
 if (token) headers.set('Authorization',`Bearer ${token}`)
 const response = await fetch(base + path, {...options, headers, signal: options.signal ?? AbortSignal.timeout(15000)})
 const payload = await response.json().catch(() => null)
 if (!response.ok || payload?.success === false) {
   const message = response.status >= 500 ? 'FinSync API returned a server error.' : payload?.message || `Request failed (HTTP ${response.status}).`
   throw Object.assign(new Error(message),{status: response.status})
 }
 return payload?.data ?? payload
}

export async function operation(controller: string, method: string, kind: 'collection'|'detail'|'login'|'summary'|'report') {
 const override = import.meta.env[`VITE_ROUTE_${controller.toUpperCase()}_${kind.toUpperCase()}_${method.toUpperCase()}`]
 if (override?.startsWith('/') && !override.startsWith('//')) return override
 paths ??= request(import.meta.env.VITE_OPENAPI_PATH || '/swagger/v1/swagger.json').then(doc => {
   if (!doc?.paths) throw new Error('The API did not return an OpenAPI document.')
   return doc.paths
 }).catch(error => { paths = undefined; throw error })
 const document = await paths
 const candidates = Object.entries(document).filter(([path, methods]) => {
   const endpoint = methods[method]
   if (!endpoint?.tags?.some(tag => tag.toLowerCase() === controller.toLowerCase())) return false
   if (kind === 'detail') return /\/\{[^}]+\}$/.test(path)
   if (kind === 'collection') return new RegExp(`/${controller}$`,'i').test(path)
   return new RegExp(`/${kind === 'report' ? 'dashboard' : kind}$`,'i').test(path)
 })
 if (candidates.length !== 1) throw new Error(`No unique ${controller} ${kind} route was found in OpenAPI. Check the route configuration.`)
 return candidates[0][0]
}

export function rows(value: unknown): Record<string, unknown>[] {
 if (Array.isArray(value)) return value
 if (value && typeof value === 'object') {
   const list = Object.values(value).find(Array.isArray)
   if (list) return list
   return [value as Record<string, unknown>]
 }
 return []
}
