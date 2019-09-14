import { Component, OnInit } from '@angular/core';
import { EditAthleteresult } from '../models/editAthleteResult';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../admin.service';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { asapScheduler } from 'rxjs';
import { Athlete } from '../models/athlete';
import { Result } from '../models/result';
import { Location } from '@angular/common';

@Component({
  selector: 'app-test-edit',
  templateUrl: './test-edit.component.html',
  styleUrls: ['./test-edit.component.css']
})
export class TestEditComponent implements OnInit {
  testForm: FormGroup;
  editTest: EditAthleteresult;
  athlete: Athlete[];
  result: Result;
  resultid: number;
  constructor(private route: ActivatedRoute,
              private service: AdminService,
              private fb: FormBuilder,
              private router: Router,
              private location: Location) { }

              get athletes(): FormArray {
                return this.testForm.get('athletes') as FormArray;
              }

  ngOnInit() {
   this.testForm = this.fb.group({
        userid: ['' , Validators.required],
      distance: ['', Validators.required]

   });

   const id = +this.route.snapshot.paramMap.get('resultId');
   this.service.getEditTest(id).subscribe(
     data => {
       this.editTest = data;
     //  console.log(this.editTest.athletes[0].id);
       this.patch();
       this.resultid = this.editTest.result.id;
       console.log('testid=' + this.resultid);
    }
   );

  }

  patch(): void {
    this.testForm.patchValue({
      distance: this.editTest.result.distance,
       userid: this.editTest.result.userId
     });
    this.athlete = this.editTest.athletes;
   // this.testForm.setControl('athletes', this.fb.array(this.editTest.athletes));
  }

  onSave(): void {
   console.log('In onSave form value=' + this.testForm.get('userid').value);
   this.result = this.editTest.result;
   this.result.userId = this.testForm.get('userid').value;
   this.result.distance = this.testForm.get('distance').value;
  // var p = { ...this.editTest.result, ...this.testForm.value};
   // this.editTest.result.userid = this.testForm.controls.userid.value;
  //  this.editTest.result.distance = this.testForm.controls.distance.value;
  // console.log("(In onSave)before save() = "+this.result.user_id);
   this.save();
   console.log('(In onSave)data=' +  this.result.userId);
  }
  save() {
  //  console.log("save() = "+this.result);
    this.service.updateResult(this.result).subscribe(
  () =>  this.location.back()
    );

  }

}
