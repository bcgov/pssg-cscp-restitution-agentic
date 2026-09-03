import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { NotFoundComponent } from './not-found.component';

describe('NotFound component', () => {
  let fixture: ComponentFixture<NotFoundComponent>;
  let router: { navigateByUrl: ReturnType<typeof vi.fn> };

  beforeEach(async () => {
    router = { navigateByUrl: vi.fn() };

    await TestBed.configureTestingModule({
      declarations: [NotFoundComponent],
      providers: [{ provide: Router, useValue: router }]
    }).compileComponents();
  });

  it('navigates to the 404 route when constructed', () => {
    fixture = TestBed.createComponent(NotFoundComponent);

    expect(fixture.componentInstance).toBeTruthy();
    expect(router.navigateByUrl).toHaveBeenCalledWith('/404');
  });
});
