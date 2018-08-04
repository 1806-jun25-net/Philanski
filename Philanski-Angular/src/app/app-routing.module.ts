import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component'
import { Login } from './models/Login';
import { AppComponent } from './app.component';
import { ManagerComponent } from './account/manager/manager.component';


const routes: Routes = [
  { path: 'login', component: LoginComponent },

  { path: 'home', component: AppComponent},

  { path: 'logout', redirectTo: 'https://localhost:44382/', pathMatch: 'full'},

  { path: 'manager', component: ManagerComponent}
  //{ path: 'tsa', component: tsaComponent }

];


@NgModule({

  imports: [ RouterModule.forRoot(routes) ],
  exports: [RouterModule]
})
export class AppRoutingModule { }




