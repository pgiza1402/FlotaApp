import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarsArchivalListComponent } from './cars-archival-list.component';

describe('CarsArchivalListComponent', () => {
  let component: CarsArchivalListComponent;
  let fixture: ComponentFixture<CarsArchivalListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CarsArchivalListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CarsArchivalListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
