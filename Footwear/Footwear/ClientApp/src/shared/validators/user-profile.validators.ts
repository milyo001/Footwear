import { FormControl } from '@angular/forms';

/**
 * A custom validator used to return 'passwordsMismatch' error property when old password and new password fields are different
 */
export function validateOldAndNewPass(control: FormControl) {
  if (control.parent == null) {
    return null;
  }
  let pass = control.parent.get('password').value;
  let newPass = control.value;
  return pass === newPass ? { passwordsMismatch: true } : null;
}

/**
 * A custom validator used to return 'confPasswordMismatch' error property when new password and confirm password fields are different
 */
export function validateNewAndConfPass(control: FormControl) {
  if (control.parent == null) {
    return null;
  }
  let newPass = control.parent.get('newPassword').value;
  let confPass = control.value;
  return newPass !== confPass ? { confPasswordMismatch: true } : null;
}

/**
 * A custom validator used to return 'emailMismatch' error property when email and confirm email fields are different
 */
export function valideEmails(control: FormControl) {
  if (control.parent == null) {
    return null;
  }
  let email = control.parent.get('email').value;
  let confEmail = control.value;
  return email !== confEmail ? { emailMismatch: true } : null;
}
