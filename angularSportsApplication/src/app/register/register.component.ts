import { Component, OnInit } from '@angular/core';
import { FormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterModel } from './registerModel';
import { JsonPipe } from '@angular/common';
import { AppmoduleService } from '../appmodule.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  Model: RegisterModel;
  constructor(private fb: FormBuilder,
              private service: AppmoduleService,
              private router: Router ) { }

  ngOnInit() {
    this.registerForm = this.fb.group({
     email: ['', Validators.required ],
     password: ['', Validators.required],
     confirmPassword: [''],
     role: ['', Validators.required]
    });
  }

  onSubmit(): void {
    this.Model = this.registerForm.value;
    console.log('form gh data=' + JSON.stringify(this.Model));
    this.service.register(this.Model).subscribe(
      data => {console.log(data); },
      error => {console.log(error); }
      );
    this.router.navigate(['/login']);
  }

}
