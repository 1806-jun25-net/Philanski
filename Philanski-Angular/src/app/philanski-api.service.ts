import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Login } from './models/Login';
import { TimeSheetApproval } from './models/TimeSheetApproval'

@Injectable({
  providedIn: 'root'
})
export class PhilanskiApiService {

  private readonly Url : string = 'https://localhost:44386/api/'
  //private readonly Url : string = 'https://philanksi.azurewebsites.net/api/'
  constructor(private httpClient: HttpClient) { }


  postLogin(login: Login) {
    var body = JSON.stringify(login)
    var header = new HttpHeaders({ 
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Headers': 'Content-Type, Origin , Access-Control-* , X-Requested-With, Accept',
    'Content-Type':  'application/json,charset=utf-8',
    'Accept': 'application/json',
    'Allow' : 'GET, POST, PUT, DELETE, OPTIONS, HEAD'
  });
    return this.httpClient.post<Login>(this.Url + 'Account/Login', body, {headers: header, withCredentials: true, observe: 'response'})
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
    var header = new HttpHeaders({ 
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Headers': 'Content-Type, Origin , Access-Control-* , X-Requested-With, Accept',
      'Content-Type':  'application/json,charset=utf-8',
      'Accept': 'application/json',
      'Allow' : 'GET, POST, PUT, DELETE, OPTIONS, HEAD'
    })
    return this.httpClient.post(this.Url + 'Account/Logout', {headers: header, withCredentials: true, observe: 'response'} )
  }
  
  getTSAsForApproval()
  {
    var header = new HttpHeaders({ 
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Headers': 'Content-Type, Origin , Access-Control-* , X-Requested-With, Accept',
      'Content-Type':  'application/json,charset=utf-8',
      'Accept': 'application/json',
      'Allow' : 'GET, POST, PUT, DELETE, OPTIONS, HEAD'
    })
    return this.httpClient.get<TimeSheetApproval[]>(this.Url + 'Manager/' + sessionStorage.getItem('UserName') + '/TimeSheetApproval', {headers: header, withCredentials: true, observe: 'response'})
  }

}
