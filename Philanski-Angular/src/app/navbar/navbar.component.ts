import { Component, OnInit, Input } from '@angular/core';
import { User } from '../user';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  @Input() user: User;

  constructor(
    private route: ActivatedRoute
  ) {}
  userName = sessionStorage.getItem('UserName')
  ngOnInit() {
  }

  getUser(): void {
    const email = +this.route.snapshot.paramMap.get('email');
    this.heroService.getHero(id)
      .subscribe(hero => this.hero = hero);
  }

}
