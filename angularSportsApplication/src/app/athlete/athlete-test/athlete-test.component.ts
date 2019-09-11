import { Component, OnInit } from '@angular/core';
import { AthleteService } from '../athlete.service';
import { AthleteDetail } from '../model/athleteDetail';

@Component({
  selector: 'app-athlete-test',
  templateUrl: './athlete-test.component.html',
  styleUrls: ['./athlete-test.component.css']
})
export class AthleteTestComponent implements OnInit {
  test: AthleteDetail;
  constructor(private service: AthleteService) { }

  ngOnInit() {
    this.service.getAthleteTests().subscribe(
      data => { this.test = data;
                console.log(JSON.stringify('Atghlte data' + data));
              }

    );
  }

}
