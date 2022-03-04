import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InsurancesListComponent } from './insurances-list.component';

describe('InsurancesListComponent', () => {
  let component: InsurancesListComponent;
  let fixture: ComponentFixture<InsurancesListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InsurancesListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InsurancesListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
