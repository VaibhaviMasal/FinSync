import React, {useEffect,useState} from 'react'
import {createRoot} from 'react-dom/client'
import {Activity,ArrowDownToLine,ArrowRight,ArrowUpRight,Bell,Building2,CalendarDays,CarFront,ChartNoAxesCombined,ChevronDown,ChevronLeft,ChevronRight,CircleDollarSign,ClipboardCheck,FileText,LayoutDashboard,Menu,Plus,Search,Settings,Shield,ShieldCheck,Users,UserRound,WalletCards,X} from 'lucide-react'
import {base,operation,request,rows} from './api'
import {demo,modules,label,money} from './data'
import './style.css'

const icons=[Users,CarFront,Building2,WalletCards,ShieldCheck,CircleDollarSign,CalendarDays,ClipboardCheck,UserRound,FileText,Bell,ChartNoAxesCombined,Settings]
const read = key => {try{return JSON.parse(sessionStorage.getItem(key))}catch{return null}}
const route = () => location.hash.slice(2) || 'dashboard'
const initialDemo = () => read('finsync-demo') || demo
const columns = entry => entry[4].split(',')
const navigate = path => {location.hash = '/' + path}
const config = id => modules.find(module => module[0] === id)
const date = new Intl.DateTimeFormat('en-IN',{day:'numeric',month:'short',year:'numeric'})
const groups=[['Workspace',['dashboard','customers','vehicles']],['Insurance',['policies','companies','plans','payments','renewals','claims']],['Administration',['agents','documents','notifications','reports','settings']]]
const singular={customers:'customer',policies:'policy',companies:'company',plans:'plan',payments:'payment',renewals:'renewal',claims:'claim',agents:'agent',vehicles:'vehicle',documents:'document'}
const amounts = /amount|premium/i
const dateFields = /date$/i

function FieldValue({name,value}) {
 if(value==null || value==='') return '—'
 if(name.endsWith('Status') || typeof value==='boolean'){
   const status=typeof value==='boolean' ? (name==='isRead' ? (value?'Read':'Unread') : (value?'Active':'Inactive')) : String(value)
   const tone=/^(paid|active|approved|settled)$/i.test(status)?'green':/pending|review|unread/i.test(status)?'amber':'neutral'
   return <span className={'badge '+tone}><i/>{status}</span>
 }
 if(amounts.test(name) && typeof value==='number') return money(value)
 if(dateFields.test(name) && !Number.isNaN(Date.parse(value))) return date.format(new Date(value))
 return typeof value==='object' ? JSON.stringify(value) : String(value)
}

function Modal({title,onClose,children}) {
 function onKey(event){if(event.key==='Escape'){event.stopPropagation();onClose()}}
 return <div className="overlay" onClick={onClose}><section className="modal" role="dialog" aria-modal="true" aria-label={title} onClick={e=>e.stopPropagation()} onKeyDown={onKey}><header><h2>{title}</h2><button autoFocus className="icon" aria-label="Close dialog" onClick={onClose}><X size={20}/></button></header>{children}</section></div>
}

function Table({items,fields,onDetail}) {
 if(!items.length)return <div className="empty"><FileText size={25}/><strong>No records found</strong><span>Records will appear here when available.</span></div>
 return <div className="scroll"><table><thead><tr>{fields.map(field=><th key={field}>{label(field)}</th>)}<th aria-label="Actions"/></tr></thead><tbody>{items.map((item,index)=><tr key={index}>{fields.map(field=><td key={field}><FieldValue name={field} value={item[field]}/></td>)}<td><button className="icon" aria-label={`View record ${index+1}`} onClick={()=>onDetail(item)}><ChevronRight size={17}/></button></td></tr>)}</tbody></table></div>
}

function PremiumChart(){
 const [period,setPeriod]=useState('6')
 const points=period==='6'?[2.15,3.25,2.98,4.28,3.87,4.83]:[4.28,3.87,4.83]
 const names=(period==='6'?['Jun','Jul','Aug','Sep','Oct','Nov']:['Sep','Oct','Nov'])
 const coords=points.map((value,i)=>[55+i*570/(points.length-1),210-value*35])
 const line=coords.map(([x,y],i)=>i===0?`M${x},${y}`:`C${(coords[i-1][0]+x)/2},${coords[i-1][1]} ${(coords[i-1][0]+x)/2},${y} ${x},${y}`).join(' ')
 return <article className="panel chart"><div className="panel-head"><div><h2>Premium collection</h2><p>Monthly premium received</p></div><select aria-label="Chart period" value={period} onChange={e=>setPeriod(e.target.value)}><option value="6">Last 6 months</option><option value="3">Last 3 months</option></select></div><svg viewBox="0 0 650 246" role="img" aria-label="Illustrative monthly premium totals in lakh rupees"><defs><linearGradient id="area" x1="0" x2="0" y1="0" y2="1"><stop stopColor="#4375ee" stopOpacity=".23"/><stop offset="1" stopColor="#4375ee" stopOpacity=".02"/></linearGradient></defs>{[0,1,2,3,4,5].map(t=><g key={t}><text x="2" y={214-t*35}>{t?'₹'+t+'L':'₹0'}</text><line x1="52" x2="628" y1={210-t*35} y2={210-t*35}/></g>)}<path d={line+' L625,210 L55,210 Z'} fill="url(#area)"/><path d={line} fill="none" stroke="#3368eb" strokeWidth="3"/>{coords.map(([x,y],i)=><g key={x}><circle cx={x} cy={y} r="4" fill="white" stroke="#3368eb" strokeWidth="2"><title>{names[i]} {money(points[i]*100000)}</title></circle><text textAnchor="middle" x={x} y="235">{names[i]}</text></g>)}</svg></article>
}

function Dashboard({isDemo,items,summary,onDetail}){
 const stats=[
  ['Total customers',isDemo?'1,248':summary?.totalCustomers,Users,'blue','+8.2%'],
  ['Active policies',isDemo?'864':summary?.activePolicies,ShieldCheck,'purple','+5.4%'],
  ['Premium collected',isDemo?'₹24.86L':undefined,CircleDollarSign,'mint','+12.6%'],
  ['Renewals due',isDemo?'32':summary?.policiesExpiringSoon,CalendarDays,'orange','Next 30 days'],
 ]
 return <><div className="stats">{stats.map(([title,value,Icon,tone,note])=><article className="panel stat" key={title}><span className={'tile '+tone}><Icon size={20}/></span><div className="stat-name">{title}</div><strong>{value??'—'}</strong>{isDemo&&<div className="trend">{note.startsWith('+')&&<ArrowUpRight size={13}/>} {note} {note.startsWith('+')&&<small>vs. last month</small>}</div>}</article>)}</div>
 {isDemo&&<div className="insights"><PremiumChart/><article className="panel mix"><div className="panel-head"><div><h2>Policy breakdown</h2><p>Active policies by coverage</p></div></div><div className="donut"><div><strong>864</strong><span>Active policies</span></div></div><div className="legend">{[['Health insurance','42%','#3368eb'],['Motor insurance','28%','#28a996'],['Life insurance','19%','#f2b260'],['Other','11%','#a18cea']].map(([title,value,color])=><div key={title}><span><i style={{background:color}}/>{title}</span><strong>{value}</strong></div>)}</div></article></div>}
 <article className="panel"><div className="panel-head"><div><h2>Recent policies</h2><p>Latest policy records in your workspace</p></div><button className="secondary" onClick={()=>navigate('policies')}>View policies <ArrowRight size={15}/></button></div><Table items={items.slice(0,5)} fields={columns(config('policies'))} onDetail={onDetail}/></article></>
}

function Editor({entry,item,onSave,onClose}) {
 const fields=columns(entry)
 const [values,setValues]=useState(()=>Object.fromEntries(fields.map(field=>[field,item?.[field] ?? (field.startsWith('is')?false:'')])))
 return <Modal title={(item?'Edit ':'New ')+singular[entry[0]]} onClose={onClose}><form className="editor" onSubmit={event=>{event.preventDefault();onSave(values)}}>{fields.map((field,i)=><label key={field}>{label(field)}{typeof values[field]==='boolean'?<input type="checkbox" checked={values[field]} onChange={e=>setValues({...values,[field]:e.target.checked})}/>:<input required={i===0} type={dateFields.test(field)?'date':amounts.test(field)?'number':field==='email'?'email':'text'} min={amounts.test(field)?0:undefined} step={amounts.test(field)?'0.01':undefined} value={values[field]} onChange={e=>setValues({...values,[field]:amounts.test(field)&&e.target.value!==''?Number(e.target.value):e.target.value})}/>}</label>)}<div className="form-actions"><button type="button" className="secondary" onClick={onClose}>Cancel</button><button className="primary">Save record</button></div></form></Modal>
}

function Login({onLogin,onDemo}) {
 const [error,setError]=useState(''),[busy,setBusy]=useState(false)
 async function submit(event){
  event.preventDefault();setError('');setBusy(true)
  const fields=new FormData(event.currentTarget)
  try{
   const path=await operation('Authentication','post','login')
   const data=await request(path,{method:'POST',body:JSON.stringify({email:fields.get('email'),password:fields.get('password')})})
   const token=data?.token ?? data?.accessToken
   if(typeof token!=='string')throw new Error('The login response did not include a token.')
   const encoded=token.split('.')[1]?.replace(/-/g,'+').replace(/_/g,'/')
   const claims=JSON.parse(new TextDecoder().decode(Uint8Array.from(atob(encoded),c=>c.charCodeAt(0))))
   onLogin({token,name:claims.name??claims['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']??fields.get('email'),role:claims.role??claims['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']??'User',expiry:claims.exp})
  }catch(e){setError(e.message)}finally{setBusy(false)}
 }
 return <main className="login"><form className="panel editor" onSubmit={submit}><div className="brand"><span className="brand-mark"><ShieldCheck size={22}/></span>FinSync</div><h1>Sign in</h1><p>Use your existing FinSync account.</p>{error&&<p className="error" role="alert">{error}</p>}<label>Email<input name="email" type="email" required autoComplete="username"/></label><label>Password<input name="password" type="password" required autoComplete="current-password"/></label><button className="primary" disabled={busy}>{busy?'Signing in…':'Sign in'}</button><button type="button" className="secondary" onClick={onDemo}>Open demo workspace</button></form></main>
}

function App(){
 const [path,setPath]=useState(route),[collapsed,setCollapsed]=useState(false),[mobile,setMobile]=useState(false)
 const [session,setSession]=useState(()=>{const user=read('finsync-session');return user?.apiBase===base && (!user.expiry || user.expiry*1000>Date.now())?user:null})
 const [isDemo,setDemo]=useState(!base),[dataset,setDataset]=useState(initialDemo),[live,setLive]=useState({items:[],summary:null,loading:false,error:''})
 const [detail,setDetail]=useState(null),[editing,setEditing]=useState(null),[query,setQuery]=useState(''),[globalQuery,setGlobal]=useState(''),[page,setPage]=useState(1),[status,setStatus]=useState('All'),[profile,setProfile]=useState(false),[retry,setRetry]=useState(0),[notice,setNotice]=useState('')
 const entry=config(path)
 function login(user){const current={...user,apiBase:base};sessionStorage.setItem('finsync-session',JSON.stringify(current));setSession(current);setDemo(false)}
 function logout(){sessionStorage.removeItem('finsync-session');setSession(null);setDemo(false);setProfile(false)}
 useEffect(()=>{const update=()=>{setPath(route());setQuery('');setGlobal('');setStatus('All');setPage(1);setDetail(null);setEditing(null);setMobile(false);setNotice('')};window.addEventListener('hashchange',update);return()=>window.removeEventListener('hashchange',update)},[])
 useEffect(()=>{if(!notice)return;const timeout=setTimeout(()=>setNotice(''),3500);return()=>clearTimeout(timeout)},[notice])
 useEffect(()=>{if(!session?.expiry)return;const timeout=setTimeout(logout,Math.max(0,session.expiry*1000-Date.now()));return()=>clearTimeout(timeout)},[session])
 useEffect(()=>{
  if(isDemo||!session)return
  let active=true
  const control=new AbortController()
  setLive({path,items:[],summary:null,loading:true,error:''})
  const get=async (controller,kind)=>request(await operation(controller,'get',kind),{signal:AbortSignal.any([control.signal,AbortSignal.timeout(15000)])},session.token)
  async function load(){
   try{
    if(path==='vehicles')throw new Error('The backend does not expose a standalone vehicle list.')
    const result=path==='dashboard'?await Promise.allSettled([get('Policy','collection'),get('Dashboard','summary')]):null
    if(result){
     if(result.some(r=>r.status==='rejected'&&r.reason.status===401)){if(active)logout();return}
     const [policies,summary]=result
     if(active)setLive({path,items:policies.status==='fulfilled'?rows(policies.value):[],summary:summary.status==='fulfilled'?summary.value:null,loading:false,error:result.some(r=>r.status==='rejected')?'Some dashboard data could not be loaded.':''})
    }else if(entry){
     const data=await get(entry[2],path==='reports'?'report':'collection')
     if(active)setLive({path,items:rows(data),summary:null,loading:false,error:''})
    }
   }catch(error){if(active){if(error.status===401)logout();else setLive({path,items:[],summary:null,loading:false,error:error.message})}}
  }
  load()
  return()=>{active=false;control.abort()}
 },[path,session,isDemo,retry])
 if(!isDemo&&!session)return base?<Login onLogin={login} onDemo={()=>setDemo(true)}/>:<div className="login"><div className="panel editor"><h1>FinSync</h1><p>Connect an API in your environment to sign in.</p><button className="primary" onClick={()=>setDemo(true)}>Open demo workspace</button></div></div>
 const dashboard=path==='dashboard'
 const title=dashboard?'Dashboard':entry?.[1]||'Page not found'
 const items=isDemo?(dataset[dashboard?'policies':path]||[]):live.path===path?live.items:[]
 const fields=path==='reports'&&!isDemo&&items.length?Object.keys(items[0]):entry?columns(entry):[]
 const statusField=fields.find(field=>field.endsWith('Status'))
 const statuses=statusField?[...new Set(items.map(item=>item[statusField]).filter(Boolean))]:[]
 const visible=items.filter(item=>fields.some(field=>String(item[field]??'').toLowerCase().includes(query.trim().toLowerCase()))&&(status==='All'||item[statusField]===status))
 const searchable=globalQuery.trim().toLowerCase()
 const matches=searchable?modules.filter(module=>module[1].toLowerCase().includes(searchable)||isDemo&&dataset[module[0]]?.some(item=>columns(module).some(field=>String(item[field]??'').toLowerCase().includes(searchable)))):[]
 const editable=isDemo&&Boolean(singular[path])
 function persist(records){const updated={...dataset,[path]:records};sessionStorage.setItem('finsync-demo',JSON.stringify(updated));setDataset(updated)}
 function save(values){
  const id=entry[3]
  if(editing.item)persist(items.map(item=>item[id]===editing.item[id]?{...item,...values}:item))
  else persist([{...values,[id]:crypto.randomUUID()},...items])
  setEditing(null);setDetail(null);setPage(1);setNotice('Record saved in demo workspace.')
 }
 function exportRecords(){
  const cell=value=>'"'+String(value??'').replace(/^[=+@\-\t\r]/,"'$&").replaceAll('"','""')+'"'
  const csv=[fields.map(field=>cell(label(field))).join(','),...visible.map(item=>fields.map(field=>cell(item[field])).join(','))].join('\r\n')
  const url=URL.createObjectURL(new Blob([csv],{type:'text/csv;charset=utf-8'}))
  const link=document.createElement('a');link.href=url;link.download=`finsync-${path}.csv`;link.click();setTimeout(()=>URL.revokeObjectURL(url),1000)
 }
 const name=isDemo?'Alex Morgan':session.name
 return <div className={'app '+(collapsed?'compact':'')}>
  {mobile&&<button className="scrim" aria-label="Close navigation" onClick={()=>setMobile(false)}/>}
  <aside className={mobile?'sidebar visible':'sidebar'}>
   <a className="brand" href="#/dashboard"><span className="brand-mark"><ShieldCheck size={22}/></span><span>FinSync<small>Insurance CRM</small></span></a>
   <nav aria-label="Main navigation">{groups.map(([group,keys])=><div className="nav-group" key={group}><h3>{group}</h3>{keys.map(key=>{const index=modules.findIndex(x=>x[0]===key),Icon=key==='dashboard'?LayoutDashboard:icons[index];return <a key={key} href={'#/'+key} aria-current={path===key?'page':undefined} title={key==='dashboard'?'Dashboard':modules[index][1]}><Icon size={17}/><span>{key==='dashboard'?'Dashboard':modules[index][1]}</span></a>})}</div>)}</nav>
   <button className="collapse" onClick={()=>{if(mobile)setMobile(false);else setCollapsed(!collapsed)}}>{collapsed?<ChevronRight size={17}/>:<ChevronLeft size={17}/>}<span>Collapse sidebar</span></button>
  </aside>
  <div className="workspace">
   <header className="top"><div className="crumb"><button className="icon mobile-menu" aria-label="Open navigation" onClick={()=>setMobile(!mobile)}><Menu size={21}/></button><span>Workspace</span><ChevronRight size={14}/><strong>{title}</strong></div>
    <div className="top-actions"><div className="global"><Search size={17}/><input aria-label="Search workspace" placeholder="Search workspace..." value={globalQuery} onChange={e=>setGlobal(e.target.value)} onKeyDown={e=>{if(e.key==='Escape')setGlobal('');if(e.key==='Enter'&&matches.length)navigate(matches[0][0])}}/>{searchable&&<div className="popover">{matches.length?matches.map(module=><button key={module[0]} onClick={()=>{navigate(module[0]);setGlobal('')}}>{module[1]} <ArrowRight size={14}/></button>):<p>No matching modules</p>}</div>}</div><button className="icon notification" aria-label="Notifications" onClick={()=>navigate('notifications')}><Bell size={19}/>{isDemo&&<i/>}</button><div className="profile"><button aria-expanded={profile} onClick={()=>setProfile(!profile)}><span className="avatar">{name.split(/\s+/).slice(0,2).map(s=>s[0]).join('').toUpperCase()}</span><span className="profile-name">{name}<small>{isDemo?'Workspace admin':session.role}</small></span><ChevronDown size={14}/></button>{profile&&<div className="popover"><button onClick={()=>{navigate('settings');setProfile(false)}}>Settings</button><button onClick={logout}>{isDemo?'Exit demo':'Sign out'}</button></div>}</div></div>
   </header>
   <main className="content"><div className="page-head"><div><div className="eyebrow"><i/>{isDemo?'Demo workspace':'FinSync workspace'}</div><h1>{title}</h1><p>{dashboard?'Track policy activity, premium collection, and upcoming renewals.':entry?`Browse ${entry[1].toLowerCase()} in your workspace.`:'Choose a page from the navigation.'}</p></div><div className="page-actions">{dashboard?<><button className="secondary" onClick={()=>navigate('reports')}><ChartNoAxesCombined size={16}/> Reports</button><button className="primary" onClick={()=>navigate('policies')}><Shield size={16}/> Policies</button></>:entry&&<><button className="secondary" disabled={!visible.length} onClick={exportRecords}><ArrowDownToLine size={16}/> Export CSV</button>{editable&&<button className="primary" onClick={()=>setEditing({})}><Plus size={17}/> New {singular[path]}</button>}</>}</div></div>
   {notice&&<div role="status" className="notice">{notice}</div>}{!isDemo&&live.path===path&&live.error&&<div className="error" role="alert">{live.error} {path!=='vehicles'&&<button className="secondary" onClick={()=>setRetry(retry+1)}>Retry</button>}</div>}
   {!isDemo&&(live.path!==path||live.loading)&&Boolean(entry||dashboard)?<div className="panel empty" role="status"><Activity size={22}/>Loading records…</div>:dashboard?<Dashboard isDemo={isDemo} summary={live.summary} items={items} onDetail={setDetail}/>:entry&&<article className="panel"><div className="panel-head filters"><div className="field-search"><Search size={17}/><input aria-label={`Search ${entry[1]}`} placeholder={`Search ${entry[1].toLowerCase()}…`} value={query} onChange={e=>{setQuery(e.target.value);setPage(1)}}/></div>{statuses.length>0&&<select aria-label="Filter by status" value={status} onChange={e=>{setStatus(e.target.value);setPage(1)}}><option value="All">All statuses</option>{statuses.map(value=><option key={value}>{value}</option>)}</select>}</div><Table items={visible.slice((page-1)*8,page*8)} fields={fields} onDetail={setDetail}/><footer className="pagination"><span>{visible.length} loaded record{visible.length===1?'':'s'}</span><div><button className="icon" aria-label="Previous page" disabled={page<=1} onClick={()=>setPage(page-1)}><ChevronLeft size={17}/></button>{page} / {Math.max(1,Math.ceil(visible.length/8))}<button className="icon" aria-label="Next page" disabled={page*8>=visible.length} onClick={()=>setPage(page+1)}><ChevronRight size={17}/></button></div></footer></article>}
   </main>
  </div>
  {detail&&!editing&&<Modal title={dashboard?'Policy details':title+' · Record details'} onClose={()=>setDetail(null)}><dl>{Object.entries(detail).filter(([key])=>!key.endsWith('Id')).map(([key,value])=><React.Fragment key={key}><dt>{label(key)}</dt><dd><FieldValue name={key} value={value}/></dd></React.Fragment>)}</dl>{editable&&<div className="form-actions"><button className="secondary" onClick={()=>{if(confirm('Delete this demo record?')){persist(items.filter(item=>item[entry[3]]!==detail[entry[3]]));setPage(1);setDetail(null);setNotice('Demo record deleted.')}}}>Delete</button><button className="primary" onClick={()=>setEditing({item:detail})}>Edit record</button></div>}</Modal>}
  {editing&&<Editor entry={entry} item={editing.item} onSave={save} onClose={()=>setEditing(null)}/>}
 </div>
}

createRoot(document.getElementById('root')).render(<App/>)
