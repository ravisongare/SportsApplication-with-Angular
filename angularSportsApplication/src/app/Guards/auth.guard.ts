import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate  {

  constructor(private jwtHelper: JwtHelperService ) {}
canActivate() {
 const token = localStorage.getItem('jwt');
//  console.log(token);
 if (token && !this.jwtHelper.isTokenExpired(token)) {
 const decode = this.jwtHelper.decodeToken(token);
 console.log(decode);
 console.log(decode.Role);
 return true;
} else {
  console.log('decode token false==');
  return false;
}

}

}
