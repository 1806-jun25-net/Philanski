import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router'
import { PhilanskiApiService } from '../../philanski-api.service';
import { Login } from '../../models/Login'
import { HttpErrorResponse, HttpHeaderResponse,HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  

  constructor(private apiService: PhilanskiApiService, private router: Router) { }
  login = new Login('', '')
  userName = ''
  ngOnInit() { 
  }


  onLogin(login: Login){
    this.apiService.postLogin(login).subscribe(
      (data : HttpResponse<Login> ) => {
       // console.log(data.body)
        console.log(data.status)        
        console.log("login success")
        sessionStorage.setItem('UserName', this.login.username) 
        this.userName = sessionStorage.getItem('UserName');
        this.router.navigateByUrl('/')
        
      },
      (err: HttpErrorResponse ) =>{
        console.log(err.status)
        console.log("login failed")

      }
    )
  }

  onLogout()
  {
    this.apiService.postLogout().subscribe(
      (data : HttpHeaderResponse) => {
          console.log(data.status)
          sessionStorage.clear()
          console.log("logged out success")
      },
      (err : HttpErrorResponse) => {
          console.log("error logging out")
      }
    )
  }

 
}
