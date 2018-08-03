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
  currentUserName = 'Not Logged In'
  ngOnInit() {
  }


  onLogin(login: Login){
    this.apiService.postLogin(login).subscribe(
      (data : any) => {
        console.log("login success")
        this.currentUserName = this.login.username

      },
      (err: HttpErrorResponse ) =>{
        console.log("login failed")

      }
    )
  }

  onLogout()
  {
    this.apiService.postLogout().subscribe(
      (data : any) => {
          this.currentUserName = 'Not Logged In'
          console.log("logged out success")
      },
      (err : HttpErrorResponse) => {
          console.log("error logging out")
      }
    )
  }
}
