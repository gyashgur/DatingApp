import { AlertifyService } from './../_services/alertify.service';
import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {

  };

  constructor(public authService: AuthService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  login() {
      // console.log(this.model);
      this.authService.login(this.model).subscribe(next => {  // next means that method was successful
        // console.log('Logged in successfully');
        this.alertify.success('Logged in successfully');
      }, error => {
         // console.log(error);
         this.alertify.error(error);
      });
  }

  loggedIn() {
    // const token = localStorage.getItem('token');
    // return !!token;

    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    // console.log('loged out');
    this.alertify.message('loged out');
  }
}
