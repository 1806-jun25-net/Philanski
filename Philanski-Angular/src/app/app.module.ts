import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '../../node_modules/@angular/router';

import { AppComponent } from './app.component';
import { LoginComponent } from './account/login/login.component';

import { ManagerComponent } from './account/manager/manager.component';

import { AppRoutingModule } from './/app-routing.module';

import { NavbarComponent } from './navbar/navbar.component';

import { LoginFormComponent } from './login-form/login-form.component';



@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    
    NavbarComponent

    ManagerComponent,

    LoginFormComponent

  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
