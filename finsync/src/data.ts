export type RecordData = Record<string, string | number | boolean | null>
export const modules = [
 ['customers','Customers','Customer','customerId','firstName,lastName,email,mobileNumber,city'],
 ['vehicles','Vehicles','Vehicle','vehicleId','registrationNumber,make,model,customerName'],
 ['companies','Insurance companies','InsuranceCompany','companyId','companyName,companyCode,email,isActive'],
 ['plans','Insurance plans','InsurancePlan','planId','planName,planCode,companyName,isActive'],
 ['policies','Policies','Policy','policyId','policyNumber,customerName,planName,premiumAmount,policyStatus'],
 ['payments','Premium payments','PremiumPayment','paymentId','policyNumber,amount,paymentDate,paymentStatus'],
 ['renewals','Policy renewals','PolicyRenewal','renewalId','policyNumber,renewalDate,renewalPremium,renewalStatus'],
 ['claims','Claims','Claim','claimId','claimNumber,policyNumber,claimDate,claimAmount,claimStatus'],
 ['agents','Agents','Agent','agentId','firstName,lastName,email,mobileNumber,city'],
 ['documents','Documents','Document','documentId','fileName,documentType,uploadedDate'],
 ['notifications','Notifications','Notification','notificationId','title,message,isRead'],
 ['reports','Reports','Report','id','name,value'],
 ['settings','Settings','Settings','settingId','companyName,email,mobileNumber'],
] as const

export const demo: Record<string, RecordData[]> = {
 customers: [
 {customerId:1,firstName:'Aarav',lastName:'Mehta',email:'aarav@example.com',mobileNumber:'90000 01001',city:'Mumbai'},
 {customerId:2,firstName:'Priya',lastName:'Sharma',email:'priya@example.com',mobileNumber:'90000 01002',city:'Pune'},
 {customerId:3,firstName:'Rohan',lastName:'Desai',email:'rohan@example.com',mobileNumber:'90000 01003',city:'Bengaluru'},
 {customerId:4,firstName:'Ananya',lastName:'Rao',email:'ananya@example.com',mobileNumber:'90000 01004',city:'Hyderabad'},
 ],
 vehicles:[{vehicleId:1,registrationNumber:'MH 12 AB 1042',make:'Tata',model:'Nexon',customerName:'Priya Sharma'}],
 companies:[{companyId:1,companyName:'Aster General Insurance',companyCode:'AGI',email:'support@example.com',isActive:true},{companyId:2,companyName:'Meridian Life',companyCode:'ML',email:'help@example.com',isActive:true}],
 plans:[{planId:1,planName:'Motor Comprehensive',planCode:'MOT-01',companyName:'Aster General Insurance',isActive:true},{planId:2,planName:'Term Life Plus',planCode:'LIF-01',companyName:'Meridian Life',isActive:true}],
 policies:[
 {policyId:1,policyNumber:'POL-2026-1042',customerName:'Aarav Mehta',planName:'Health insurance',premiumAmount:18500,policyStatus:'Active'},
 {policyId:2,policyNumber:'POL-2026-1041',customerName:'Priya Sharma',planName:'Motor insurance',premiumAmount:12400,policyStatus:'Active'},
 {policyId:3,policyNumber:'POL-2026-1040',customerName:'Rohan Desai',planName:'Life insurance',premiumAmount:24800,policyStatus:'Pending'},
 {policyId:4,policyNumber:'POL-2026-1039',customerName:'Ananya Rao',planName:'Health insurance',premiumAmount:15600,policyStatus:'Active'},
 ],
 payments:[{paymentId:1,policyNumber:'POL-2026-1042',amount:18500,paymentDate:'2026-11-14',paymentStatus:'Paid'},{paymentId:2,policyNumber:'POL-2026-1040',amount:24800,paymentDate:'2026-11-15',paymentStatus:'Pending'}],
 renewals:[{renewalId:1,policyNumber:'POL-2026-1041',renewalDate:'2026-11-24',renewalPremium:12400,renewalStatus:'Pending'}],
 claims:[{claimId:1,claimNumber:'CLM-2026-018',policyNumber:'POL-2026-1041',claimDate:'2026-11-14',claimAmount:32000,claimStatus:'Under review'}],
 agents:[{agentId:1,firstName:'Nisha',lastName:'Kulkarni',email:'nisha@example.com',mobileNumber:'90000 02001',city:'Pune'}],
 documents:[{documentId:1,fileName:'Policy schedule.pdf',documentType:'Policy schedule',uploadedDate:'2026-11-14'}],
 notifications:[{notificationId:1,title:'Renewal coming up',message:'POL-2026-1041 is due for renewal.',isRead:false}],
 reports:[{name:'Active policies',value:3},{name:'Collected premium',value:18500}],
 settings:[{settingId:1,companyName:'FinSync Demo',email:'team@example.com',mobileNumber:'90000 03001'}],
}
export const label = (s: string) => s.replace(/([a-z])([A-Z])/g,'$1 $2').replace(/^./,c=>c.toUpperCase())
export const money = (n: number) => new Intl.NumberFormat('en-IN',{style:'currency',currency:'INR',maximumFractionDigits:0}).format(n)
