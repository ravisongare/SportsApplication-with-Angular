import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.css']
})
export class CustomersComponent implements OnInit {
  customers: any;

  constructor(private http: HttpClient,
              private router: Router) { }

  ngOnInit() {
    const token = localStorage.getItem('jwt');
    // console.log('before ' + token);
    this.http.get('http://localhost:5000/api/customers', {
      headers: new HttpHeaders({


        Authorization: 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).subscribe(response => {
      this.customers = response;
    }, err => {
      console.log(err);
    });
  }
}
