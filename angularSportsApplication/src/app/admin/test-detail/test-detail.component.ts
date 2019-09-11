import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdminService } from '../admin.service';
import { ActivatedRoute } from '@angular/router';
import { TestDetailModel } from '../models/testDetailModel';
import { Detail } from '../models/detail';
import { Test } from '../models/test';

@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent implements OnInit {
 count = 1;
 detail: Detail[];
 test: Test;
 type: string;
 date: number;
 testid: string;
  constructor(private service: AdminService,
              private route: ActivatedRoute) { }

  ngOnInit() {
   this.testid = this.route.snapshot.paramMap.get('id');

   this.service.getestDetail(this.testid).subscribe(
     data => {
               this.detail = data.detail;
               this.test = data.test;
               this.type = this.test.type;
               this.date = this.test.date;
              },
     error => {console.log(error); }
   );
  }

}
