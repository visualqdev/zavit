export function htmlEncode(stringToEncode) {
    const replaceWith = {
        "&": "&amp;",
        "<": "&lt;",
        ">": "&gt;",
        "'": "&#39;",
        '"': "&quot;",
        "`": "&#96"
    };

    if (stringToEncode === undefined || stringToEncode === null) {
        return "";
    }

    const encoded = stringToEncode.replace(
        /[&<>'"`]/g,
        (match) => replaceWith[match]);
    return encoded;
}

export function htmlDecode(stringToDecode) {
    const replaceWith = {
        "&amp;": "&",
        "&#38;": "&",
        "&lt;": "<",
        "&#60;": "<",
        "&gt;": ">",
        "&#62;": ">",
        "&apos;": "'",
        "&#39;": "'",
        "&quot;": '"',
        "&#34;": '"',
        "&#96": "`"
    };
    
    if (stringToDecode === undefined || stringToDecode === null) {
        return "";
    }

    const decoded =stringToDecode.replace(
        /&(?:amp|#38|lt|#60|gt|#62|apos|#39|quot|#34|#96);/g,
        (match) => replaceWith[match]);
    return decoded;
}