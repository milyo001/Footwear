import { HttpClientModule } from '@angular/common/http';
import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductSelectComponent } from './product-select.component';

describe('ProductSelectComponent', () => {
  let component: ProductSelectComponent;
  let fixture: ComponentFixture<ProductSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ProductSelectComponent],
      imports: [HttpClientModule],
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProductSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
