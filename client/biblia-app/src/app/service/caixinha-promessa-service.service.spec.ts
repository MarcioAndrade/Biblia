/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { CaixinhaPromessaServiceService } from './caixinha-promessa-service.service';

describe('CaixinhaPromessaServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CaixinhaPromessaServiceService]
    });
  });

  it('should ...', inject([CaixinhaPromessaServiceService], (service: CaixinhaPromessaServiceService) => {
    expect(service).toBeTruthy();
  }));
});
