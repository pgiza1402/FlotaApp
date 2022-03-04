import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectInputUserComponent } from './select-input-user.component';

describe('SelectInputUserComponent', () => {
  let component: SelectInputUserComponent;
  let fixture: ComponentFixture<SelectInputUserComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectInputUserComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectInputUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
