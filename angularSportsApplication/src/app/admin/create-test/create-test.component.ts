import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Test } from '../models/test';
import { Location } from '@angular/common';

@Component({
  selector: 'app-create-test',
  templateUrl: './create-test.component.html',
  styleUrls: ['./create-test.component.css']
})
export class CreateTestComponent implements OnInit {

  test = new Test();
  testform: FormGroup;

  constructor(private service: AdminService,
              private router: Router,
              private route: ActivatedRoute,
              private fb: FormBuilder,
              private location: Location) { }

  ngOnInit() {
    this.testform = this.fb.group({
      type: '',
      date: ''
       });
  }

  onSave(): void {
    this.test.type = this.testform.get('type').value;
    this.test.date = this.testform.get('date').value;
    this.service.createTest(this.test).subscribe(
      () => this.location.back()
    );
  }

}
