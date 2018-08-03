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
    var header = new HttpHeaders({ 'Content-Type' : 'application/json'})
    return this.httpClient.post(this.Url + 'Account/Login', body, {headers: header})
  }

}
