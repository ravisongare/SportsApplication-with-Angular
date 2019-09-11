import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';
import { RouterModule } from '@angular/router';
import { CustomersComponent } from './customers/customers.component';
import { AuthGuard } from './Guards/auth.guard';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { AdminModule } from './admin/admin.module';
import { TestListComponent } from './admin/test-list/test-list.component';
import { AthleteModule } from './athlete/athlete.module';

@NgModule({
  declarations: [
    AppComponent,
    CustomersComponent,
    RegisterComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AdminModule,
    AthleteModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config: {
        whitelistedDomains: ['example.com'],
        blacklistedRoutes: ['example.com/examplebadroute/']
      }
    }),
RouterModule.forChild([
      {path: '', component: LoginComponent},
      { path: 'customers', component: CustomersComponent, canActivate: [AuthGuard] },
      { path: 'register', component: RegisterComponent},
      {path: 'login', component: LoginComponent}
]),
    FormsModule,
    HttpClientModule
  ],
  providers: [JwtModule],
  bootstrap: [AppComponent]
})
export class AppModule { }
