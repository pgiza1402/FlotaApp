import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TiresEditComponent } from './tires-edit.component';

describe('TiresEditComponent', () => {
  let component: TiresEditComponent;
  let fixture: ComponentFixture<TiresEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TiresEditComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TiresEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
