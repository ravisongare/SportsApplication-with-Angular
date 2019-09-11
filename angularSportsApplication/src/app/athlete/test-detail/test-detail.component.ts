import { Component, OnInit } from '@angular/core';
import { Result } from 'src/app/admin/models/result';
import { AthleteService } from '../athlete.service';
import { Router, ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-test-detail',
  templateUrl: './test-detail.component.html',
  styleUrls: ['./test-detail.component.css']
})
export class TestDetailComponent implements OnInit {
 result: Result;
 resultid: number;
  constructor(private service: AthleteService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    this.resultid = +this.route.snapshot.paramMap.get('resultid');
    this.service.getTestDetail(this.resultid).subscribe(
      data => { this.result = data; }
    );
  }

}
