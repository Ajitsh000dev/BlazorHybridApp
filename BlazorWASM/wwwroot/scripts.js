function requestPermission() {
    return new Promise((resolve, reject) => {
        Notification.requestPermission((permission) => {
            resolve(permission);
        });
    });
}

function isSupported() {
    if ("Notification" in window)
        return true;
    return false;
}

function create(title, options)
{
    return new Notification(title, options);
}