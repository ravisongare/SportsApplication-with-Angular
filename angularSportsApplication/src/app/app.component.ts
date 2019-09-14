import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, catchError, map } from 'rxjs/operators';

import { NgForm } from '@angular/forms';

import { Router } from '@angular/router';
import { AdminService } from './admin/admin.service';
import { AppmoduleService } from './appmodule.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'sportsApplication';
  invalidLogin: boolean;
  username: string;

  constructor(private http: HttpClient,
              private router: Router,
              private service: AppmoduleService,
              private loginservice: AppmoduleService ) { }

  ngOnInit(): void {
    // tslint:disable-next-line: no-unused-expression
   // setInterval( () => { this.username ;  this.islogin; }, 250);
 //   this.islogin = this.service.islogin;
  //  this.username = this.service.username;
  }

  login(): void {
    this.username = this.service.name;
  }

              logout(): void {
                localStorage.removeItem('jwt');
                this.loginservice.name = '';
                console.log('logout done');
                }
                getUserName(): string {
                  return this.loginservice.name;
                }
  // getData(): void {
  //    this.http.get<string>('http://localhost:5000/api/values')
  //    .pipe(tap(data => console.log(data))).subscribe(data => {console.log(JSON.stringify(data)); },
  //    err => {console.log(err);
  //   } );
  // }

}
