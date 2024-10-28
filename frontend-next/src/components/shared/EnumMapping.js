export class EnumMapping {
    static getGenderLabel(gender) {
        switch (gender) {
            case 1:
                return 'Maschile';
            case 2:
                return 'Femminile';
            default:
                return gender;
        }
    }
}
