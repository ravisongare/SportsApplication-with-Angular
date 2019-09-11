import { Injectable } from '@angular/core';
import { RegisterModel } from './register/registerModel';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppmoduleService {

  constructor(private http: HttpClient) { }
 // islogin = false;
  name: string;

  username(): string  {
return this.name;
  }

  register(model: RegisterModel): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<RegisterModel>('http://localhost:5000/api/account/register', model, { headers } );

   }
   islogin(): boolean {
     if (localStorage.getItem('jwt') != null) {
     return true;
     }
     return false;
   }
}
