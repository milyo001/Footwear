import { __awaiter } from "tslib";
import { browser, by, element } from 'protractor';
export class AppPage {
    navigateTo() {
        return __awaiter(this, void 0, void 0, function* () {
            return browser.get(browser.baseUrl);
        });
    }
    getTitleText() {
        return __awaiter(this, void 0, void 0, function* () {
            return element(by.css('app-root .content span')).getText();
        });
    }
}
//# sourceMappingURL=app.po.js.map