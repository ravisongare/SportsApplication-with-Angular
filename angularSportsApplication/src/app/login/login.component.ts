import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap, catchError, map } from 'rxjs/operators';

import { NgForm } from '@angular/forms';

import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { decode } from 'punycode';
import { AppmoduleService } from '../appmodule.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean;

  ngOnInit(): void {

  }

  constructor(private http: HttpClient,
              private router: Router,
              private jwtHelper: JwtHelperService,
              private service: AppmoduleService ) {}

  login(form: NgForm) {
    const credentials = JSON.stringify(form.value);
    this.http.post('http://localhost:5000/api/account/login', credentials, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      const token = (response as any).token;
      localStorage.setItem('jwt', token);
      this.invalidLogin = false;
     // this.service.islogin = true;
      // console.log(JSON.stringify(token));
      // tslint:disable-next-line: no-shadowed-variable
      const decode = this.jwtHelper.decodeToken(token);
      this.service.name = decode['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
      if (decode.Role === 'Athlete') {
      console.log('in login= ' + decode.Role);
      this.router.navigate(['athlete/test']);
      } else {
      this.router.navigate(['admin/tests']);
    }
    }, err => {
      this.invalidLogin = true;
    //  this.service.islogin = false;
    });
  }


}
