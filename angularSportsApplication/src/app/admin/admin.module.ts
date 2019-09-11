import { NgModule, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestListComponent } from './test-list/test-list.component';
import { RouterModule } from '@angular/router';
import { AuthGuard } from '../Guards/auth.guard';
import { TestDetailComponent } from './test-detail/test-detail.component';
import { TestEditComponent } from './test-edit/test-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TestResultDeleteComponent } from './test-result-delete/test-result-delete.component';
import { AddAthleteComponent } from './add-athlete/add-athlete.component';
import { DeleteTestComponent } from './delete-test/delete-test.component';
import { CreateTestComponent } from './create-test/create-test.component';



@NgModule({
  // tslint:disable-next-line: max-line-length
  declarations: [TestListComponent, TestDetailComponent, TestEditComponent, TestResultDeleteComponent, AddAthleteComponent, DeleteTestComponent, CreateTestComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule.forChild([
       {path: 'admin/tests', component: TestListComponent, canActivate: [AuthGuard]},
       {path: 'admin/tests/:id/detail', component: TestDetailComponent, canActivate: [AuthGuard]},
       {path: 'admin/tests/:resultId/edit', component: TestEditComponent , canActivate: [AuthGuard]},
       {path: 'admin/tests/result/:resultId/delete', component: TestResultDeleteComponent , canActivate: [AuthGuard]},
       {path: 'admin/add/athlete/:testid', component:  AddAthleteComponent, canActivate: [AuthGuard] },
       {path: 'admin/test/:testid/delete', component: DeleteTestComponent, canActivate: [AuthGuard]},
       {path: 'admin/test/create', component: CreateTestComponent , canActivate: [AuthGuard]}
    ])
  ]
})
export class AdminModule { }
