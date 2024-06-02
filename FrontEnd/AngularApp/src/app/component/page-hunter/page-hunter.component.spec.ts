import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageHunterComponent } from './page-hunter.component';

describe('PageHunterComponent', () => {
  let component: PageHunterComponent;
  let fixture: ComponentFixture<PageHunterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PageHunterComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PageHunterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
