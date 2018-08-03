import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
@Injectable({
  providedIn: 'root'
})
export class PhilanskiApiService {

  private readonly Url : string = 'https://localhost:44386/api/'
  //private readonly Url : string = 'https://philanksi.azurewebsites.net/api/'
  constructor(private httpClient: HttpClient) { }



}
