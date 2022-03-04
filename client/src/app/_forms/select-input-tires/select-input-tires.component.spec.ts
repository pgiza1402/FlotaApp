import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SelectInputTiresComponent } from './select-input-tires.component';

describe('SelectInputTiresComponent', () => {
  let component: SelectInputTiresComponent;
  let fixture: ComponentFixture<SelectInputTiresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SelectInputTiresComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SelectInputTiresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
