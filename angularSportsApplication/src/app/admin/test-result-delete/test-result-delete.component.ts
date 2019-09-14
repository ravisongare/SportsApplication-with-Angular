import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../admin.service';
import { Result } from '../models/result';
import { JsonpInterceptor } from '@angular/common/http';
import { Location } from '@angular/common';

@Component({
  selector: 'app-test-result-delete',
  templateUrl: './test-result-delete.component.html',
  styleUrls: ['./test-result-delete.component.css']
})
export class TestResultDeleteComponent implements OnInit {
 reultid: number;
  result: Result;
  constructor(private route: ActivatedRoute,
              private service: AdminService,
              private router: Router,
              private location: Location) { }

  ngOnInit() {
   this.reultid = +this.route.snapshot.paramMap.get('resultId');
   console.log('resultidd=' + this.reultid);
   this.service.deleteTestresult(this.reultid).subscribe(
     data => {
       this.result = data;
       console.log('data for delete' + JSON.stringify( this.result));
      }

   );
  }
  Back(): void {
    this.location.back();
  }
  deleteConfirm(): void {
 this.service.deleteTestresultConfirm(this.reultid).subscribe(
   () => { this.router.navigate(['/admin/tests/' + this.result.testId + '/detail']);

   }
 );

  }

}
