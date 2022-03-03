import { TestBed } from '@angular/core/testing';
import { Router  } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { CookieService } from 'ngx-cookie-service';
import { IndividualConfig, ToastrModule, ToastrService } from 'ngx-toastr';

import { AuthGuard } from './auth.guard';

describe('AuthGuard', () => {
    let guard: AuthGuard;
    // Make test toastr service to inject it into TestBed
    const toastrService = {
        success: (
            message?: string,
            title?: string,
            override?: Partial<IndividualConfig>
        ) => { },
        error: (
            message?: string,
            title?: string,
            override?: Partial<IndividualConfig>
        ) => { },
    };

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule, ToastrModule],
            providers: [RouterTestingModule, CookieService,
                { provide: ToastrService, useValue: toastrService }]
        });
        guard = TestBed.inject(AuthGuard);
    });

    it('AuthGuard should be created', () => {
        expect(guard).toBeTruthy();
    });

    it('#canActivate should be created', () => {
        expect(guard.canActivate).toBeTruthy();
    });

});
