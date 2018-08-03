import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Login } from './models/Login';

@Injectable({
  providedIn: 'root'
})
export class PhilanskiApiService {

  private readonly Url : string = 'https://localhost:44386/api/'
  //private readonly Url : string = 'https://philanksi.azurewebsites.net/api/'
  constructor(private httpClient: HttpClient) { }

  postLogin(login: Login){
    var body = JSON.stringify(login)
    var header = new HttpHeaders({ 
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Headers': 'Content-Type, Origin , Access-Control-* , X-Requested-With, Accept',
    'Content-Type':  'application/json,charset=utf-8',
    'Accept': 'application/json',
    'Allow' : 'GET, POST, PUT, DELETE, OPTIONS, HEAD'
  })
    return this.httpClient.post(this.Url + 'Account/Login', body, {headers: header, withCredentials: true} )
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
    return this.httpClient.post(this.Url + 'Account/Logout', {headers: header, withCredentials: true} )
  }
  

}
