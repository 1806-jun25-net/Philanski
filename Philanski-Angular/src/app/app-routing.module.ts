import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component'
import { Login } from './models/Login';
import { AppComponent } from './app.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: AppComponent}
];


@NgModule({

  imports: [ RouterModule.forRoot(routes) ],
  exports: [RouterModule]
})
export class AppRoutingModule { }




