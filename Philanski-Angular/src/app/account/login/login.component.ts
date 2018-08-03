import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { PhilanskiApiService } from '../../philanski-api.service';
import { Login } from '../../models/Login'
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private apiService: PhilanskiApiService) { }
  login = new Login('Username', 'Password')
  success : number = 0;
  ngOnInit() {
  }


  onLogin(login: Login){
    this.apiService.postLogin(login).subscribe(
      (data : any) => {
        this.success = 1
        console.log(this.success)
      },
      (err: HttpErrorResponse ) =>{
        this.success = 2
        console.log(this.success)
      }
    )
  }
}
