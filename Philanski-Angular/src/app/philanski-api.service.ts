import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Login } from './models/Login';
import { TimeSheetApproval } from './models/TimeSheetApproval'

@Injectable({
  providedIn: 'root'
})
export class PhilanskiApiService {

  private readonly Url : string = 'https://localhost:44386/api/'
//  private readonly Url : string = 'https://philanksi.azurewebsites.net/api/'
  private readonly header = new HttpHeaders({ 
  //  'Access-Control-Allow-Origin': 'true',
   // 'Access-Control-Allow-Credentials':'true',
    'Access-Control-Allow-Origin': 'https://localhost:44386',
    'Access-Control-Allow-Headers': 'Content-Type, Origin , Access-Control-* , X-Requested-With, Accept',
    'Content-Type':  'application/json,charset=utf-8 , application/x-www-form-urlencoded',
    'Accept': 'application/json',
    'Allow' : 'GET, POST, PUT, DELETE, OPTIONS, HEAD'
  });

  constructor(private httpClient: HttpClient) { }


  postLogin(login: Login) {
    let body = JSON.stringify(login)

    return this.httpClient.post<Login>(this.Url + 'Account/Login', body, {headers: this.header,  withCredentials: true, observe: 'response'})
         /*  .pipe(map(data : any) => {
            data.json();
            // the console.log(...) line prevents your code from working 
            // either remove it or add the line below (return ...)
            console.log("I CAN SEE DATA HERE: ", data.json());
            return data.json();
    });*/
}
  postLogout()
  {
    return this.httpClient.post(this.Url + 'Account/Logout', {headers: this.header, withCredentials: true,   observe: 'response'} )
  }
  
  getTSAsForApproval()
  {
    return this.httpClient.get<TimeSheetApproval[]>(this.Url + 'Manager/' + sessionStorage.getItem('UserName') + '/TimeSheetApproval', {headers: this.header,  withCredentials: true, observe: 'response'})
  }
  putTSA(TSA: TimeSheetApproval)
  {
    let body = JSON.stringify(TSA)
    let weekstart = TSA.weekStart.slice(8,10) + TSA.weekStart.slice(4,7) + '-' + TSA.weekStart.slice(0,4)
    return this.httpClient.put<TimeSheetApproval>(this.Url + 'Manager/' + sessionStorage.getItem('UserName') + '/TimeSheetApproval/' + weekstart + '/Employee/' + TSA.employeeId, body, {headers: this.header,  withCredentials: true, observe: 'response'} )
  }

}
