import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { Test } from '../models/test';

@Component({
  selector: 'app-test-list',
  templateUrl: './test-list.component.html',
  styleUrls: ['./test-list.component.css']
})
export class TestListComponent implements OnInit {
  Tests: Test;
  constructor(private service: AdminService) { }

  ngOnInit() {
    this.service.getTests().subscribe(
      data => {
        console.log('data=' + data);
        this.Tests = data;
        },
      error => {console.log('list error' + error); }
    );
  }

}
