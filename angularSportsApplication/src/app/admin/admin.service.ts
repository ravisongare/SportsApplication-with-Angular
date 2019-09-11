import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Test } from './models/test';
import { Observable, observable } from 'rxjs';
import { TestDetailModel } from './models/testDetailModel';
import { EditAthleteresult } from './models/editAthleteResult';
import { Result } from './models/result';
import { Athlete } from './models/athlete';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  url = 'http://localhost:5000/api/admin';

  constructor(private http: HttpClient) { }

 getTests(): Observable<Test> {
  const token = localStorage.getItem('jwt');
  return this.http.get<Test>(`${this.url}/test/list`, {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });
}

getestDetail(id: string): Observable<TestDetailModel> {
  const token = localStorage.getItem('jwt');
  return this.http.get<TestDetailModel>(`${this.url}/test/${id}/detail`, {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });
}

getEditTest(id: number): Observable<EditAthleteresult> {
  const token = localStorage.getItem('jwt');
  return this.http.get<EditAthleteresult>(`${this.url}/test/${id}/edit`, {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });
}

updateResult( result: Result): Observable<any> {
  const token = localStorage.getItem('jwt');

  return   this.http.put<Result>(`${this.url}/test/edit`, result, {
           headers: new HttpHeaders({
           Authorization: 'Bearer ' + token,
          'Content-Type': 'application/json'
    })
 });
}

deleteTestresult(resultId: number ): Observable<any> {
  const token = localStorage.getItem('jwt');
  console.log('In service result id=' + resultId);
  return this.http.get<Result>(`${this.url}/test/result/${resultId}/delete`, {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });

}

deleteTestresultConfirm(resultId: number ): Observable<any> {
  const token = localStorage.getItem('jwt');
  return this.http.delete<Result>(`${this.url}/test/result/${resultId}/delete`, {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });

}


getAllAthletes(): Observable<Athlete> {
  const token = localStorage.getItem('jwt');
  return this.http.get<Athlete>(`${this.url}/athletes`, {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });
}

addAthlete(result: Result): Observable<any> {
  const token = localStorage.getItem('jwt');
  return this.http.post<Result>(`${this.url}/athletes`, result, {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });
}

getTest(testid: string): Observable<Test> {
  const token = localStorage.getItem('jwt');
  return this.http.get<Test>(`${this.url}/test/${testid}`, {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });
}

deleteTest(testid: string): Observable<any> {

  const token = localStorage.getItem('jwt');
  return this.http.delete<Test>(`${this.url}/test/${testid}/delete`,  {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });
}

createTest(test: Test): Observable<any> {
  const token = localStorage.getItem('jwt');
  return this.http.post<Test>(`${this.url}/test/create`, test, {
    headers: new HttpHeaders({
       Authorization: 'Bearer ' + token,
      'Content-Type': 'application/json'
    })
 });
}


}
