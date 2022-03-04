import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TiresListComponent } from './tires-list.component';

describe('TiresListComponent', () => {
  let component: TiresListComponent;
  let fixture: ComponentFixture<TiresListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TiresListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TiresListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
