import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TestResultDeleteComponent } from './test-result-delete.component';

describe('TestResultDeleteComponent', () => {
  let component: TestResultDeleteComponent;
  let fixture: ComponentFixture<TestResultDeleteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TestResultDeleteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TestResultDeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
