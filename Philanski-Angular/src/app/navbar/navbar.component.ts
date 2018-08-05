import { Component, OnInit, Input } from '@angular/core';
import { User } from '../user';
import { ActivatedRoute } from '@angular/router';
import { LoginComponent } from '../account/login/login.component';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(
    private route: ActivatedRoute
  ) {}
  
  userName = sessionStorage.getItem('UserName')

  ngOnInit() {
    var userName = sessionStorage.getItem('UserName');
  }
}
