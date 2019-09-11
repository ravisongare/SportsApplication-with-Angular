import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Test } from '../models/test';

@Component({
  selector: 'app-delete-test',
  templateUrl: './delete-test.component.html',
  styleUrls: ['./delete-test.component.css']
})
export class DeleteTestComponent implements OnInit {
 test: Test;
 testid: string;
  constructor(private service: AdminService,
              private router: Router,
              private route: ActivatedRoute, ) { }

  ngOnInit() {
    this.testid = this.route.snapshot.paramMap.get('testid');
    this.service.getTest(this.testid).subscribe(
      data => {this.test = data ; }
    );
  }
  onDelete(): void {
    this.service.deleteTest(this.testid).subscribe();
    this.router.navigate(['/admin/tests']);
  }

}
