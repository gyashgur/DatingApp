import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  // values: any;  // Used in Walking Skeleton

  constructor(private http: HttpClient) { }

  ngOnInit() {
  //  this.getValues();   // Used in Walking Skeleton
  }

  registerToggle() {
    this.registerMode = true;
  }

 /*
  Used in Walking Skeleton

  getValues() {
    this.http.get('http://localhost:5000/api/values').subscribe(response => {
      this.values = response;
    }, error => {
         console.log(error);
    });
   }  */

   cancelRegisterMode(registerMode: boolean) {
     this.registerMode = registerMode;
   }
}
