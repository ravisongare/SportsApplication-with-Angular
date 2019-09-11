import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AthleteTestComponent } from './athlete-test/athlete-test.component';
import { RouterModule } from '@angular/router';
import { AuthGuard } from '../Guards/auth.guard';
import { TestDetailComponent } from './test-detail/test-detail.component';



@NgModule({
  declarations: [AthleteTestComponent, TestDetailComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: 'athlete/test', component: AthleteTestComponent, canActivate: [AuthGuard]},
      {path: 'athlete/test/:resultid/detail', component: TestDetailComponent , canActivate: [AuthGuard]}
    ])
  ]
})
export class AthleteModule { }
