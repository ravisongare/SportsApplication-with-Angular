import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AthleteTestComponent } from './athlete-test.component';

describe('AthleteTestComponent', () => {
  let component: AthleteTestComponent;
  let fixture: ComponentFixture<AthleteTestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AthleteTestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AthleteTestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
