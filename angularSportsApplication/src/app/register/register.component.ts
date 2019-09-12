import { Component, OnInit } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { RegisterModel } from './registerModel';
import { AppmoduleService } from '../appmodule.service';
import { Router } from '@angular/router';

function passwordMatcher(c: AbstractControl): { [key: string]: boolean } | null {
  const passwordControl = c.get('password');
  const confirmPasswordControl = c.get('confirmPassword');
  if (passwordControl.pristine || confirmPasswordControl.pristine) {
    return null;
  }
  if (passwordControl.value === confirmPasswordControl.value) {
    return null;
  }
  return { 'match': true };
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  Model: RegisterModel = new RegisterModel();
  constructor(private fb: FormBuilder,
              private service: AppmoduleService,
              private router: Router ) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
     email: ['', Validators.required ],
     passwordGroup: this.fb.group({
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
     }, {validator: passwordMatcher}),
     role: ['', Validators.required]
    });
  }

  onSubmit(): void {
    console.log(this.registerForm.controls.role.value);
    this.Model.Email = this.registerForm.controls.email.value;
    this.Model.Password = this.registerForm.controls.passwordGroup.get('password').value;
    this.Model.ConfirmPassword = this.registerForm.controls.passwordGroup.get('confirmPassword').value;
    this.Model.Role = this.registerForm.controls.role.value;
    console.log('form gh data=' + JSON.stringify(this.Model));
    this.service.register(this.Model).subscribe(
      data => {console.log(data); },
      error => {console.log(error); }
      );
    this.router.navigate(['/login']);
  }

}
