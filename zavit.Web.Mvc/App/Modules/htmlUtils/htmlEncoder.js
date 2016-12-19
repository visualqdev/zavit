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

    if (typeof stringToEncode === 'string' || stringToEncode instanceof String) {
        const encoded = stringToEncode.replace(
        /[&<>'"`]/g,
        (match) => replaceWith[match]);
        return encoded;
    }

    return stringToEncode;
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

    if (typeof stringToDecode === 'string' || stringToDecode instanceof String) {
        const decoded = stringToDecode.replace(
            /&(?:amp|#38|lt|#60|gt|#62|apos|#39|quot|#34|#96);/g,
            (match) => replaceWith[match]);
        return decoded;
    }

    return stringToDecode;
}