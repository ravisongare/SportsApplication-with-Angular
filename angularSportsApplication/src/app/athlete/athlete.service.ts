import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Token } from '@angular/compiler';
import { AthleteDetail } from './model/athleteDetail';
import { Observable } from 'rxjs';
import { Result } from '../admin/models/result';

@Injectable({
  providedIn: 'root'
})
export class AthleteService {
  url = 'http://localhost:5000/api/athlete';
  constructor(private http: HttpClient) { }

   getAthleteTests(): Observable<AthleteDetail> {
    const token = localStorage.getItem('jwt');
    return  this.http.get<AthleteDetail>(`${this.url}/test`, {
            headers: new HttpHeaders({
            Authorization: 'Bearer ' + token,
            'Content-Type': 'application/json'
            })
  });
  }

  getTestDetail(resultid: number): Observable<Result> {
  const token = localStorage.getItem('jwt');
  return  this.http.get<Result>(`${this.url}/test/${resultid}/detail`, {
          headers: new HttpHeaders({
          Authorization: 'Bearer ' + token,
          'Content-Type': 'application/json'
          })
});
 }

}

