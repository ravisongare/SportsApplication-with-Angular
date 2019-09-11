import { Component, OnInit } from '@angular/core';
import { AdminService } from '../admin.service';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Athlete } from '../models/athlete';
import { Result } from '../models/result';

@Component({
  selector: 'app-add-athlete',
  templateUrl: './add-athlete.component.html',
  styleUrls: ['./add-athlete.component.css']
})
export class AddAthleteComponent implements OnInit {
   testid: string;
   athleteForm: FormGroup;
   athletes: Athlete;
   result =  new Result();
  constructor(private service: AdminService,
              private router: Router,
              private route: ActivatedRoute,
              private fb: FormBuilder) { }

  ngOnInit() {
   this.testid = this.route.snapshot.paramMap.get('testid');
   this.result.test_id = this.testid;
   this.athleteForm = this.fb.group({
     distance: '',
     user_id: ''
   });

   this.service.getAllAthletes().subscribe(
     data => {
       this.athletes = data;
       console.log(JSON.stringify(data));
      }
   );

  }

   onSave(): void {
     this.result.distance = this.athleteForm.get('distance').value;
     this.result.user_id = this.athleteForm.get('user_id').value;
     this.service.addAthlete(this.result).subscribe();
     this.router.navigate(['/admin/tests/' + this.testid + '/detail']);
  }

}
