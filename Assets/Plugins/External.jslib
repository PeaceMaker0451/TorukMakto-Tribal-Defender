mergeInto(LibraryManager.library, {

    SaveExt: function(data) {
        var dataString = UTF8ToString(data);
        var obj = JSON.parse(dataString);
        player.setData(obj, false);

    },

    SignInCheck: function() {
        if (player.getMode() === 'lite') {
            myGameInstance.SendMessage('Progress', 'isSignedInWriteFalse');
        }
    },

    SignUpExt: function() {
        if (player.getMode() === 'lite') {
            ysdk.auth.openAuthDialog().then(() => {
                location.reload()
            }).catch(() => {
                console.log('Не удалось войти');
            });
        }

    },

    // SignUpExt: function() {
    //     ysdk.auth.openAuthDialog().then(() => {
    //         LoadExt();
    //         myGameInstance.SendMessage('EventSystem', 'SignUpYandexSuccess');
    //         // initPlayer({
    //         //     scopes: true
    //         // }).then(_player => {
    //         //location.reload()
    //         initPlayer().catch(err => {
    //             // Ошибка при инициализации объекта Player.
    //         });
    //     }).catch(() => {
    //         auth();
    //         myGameInstance.SendMessage('EventSystem', 'SignUpYandexSuccess');
    //         console.log('Не удалось войти');
    //     });
    // },

    LoadExt: function() {
        player.getData().then(_data => {
            const dataJSON = JSON.stringify(_data);
            myGameInstance.SendMessage('Progress', 'DataLoad', dataJSON);
        });
    },

    DeviceCheck: function() {
        if (ysdk.deviceInfo.isDesktop()) {
            myGameInstance.SendMessage('Progress', 'DeviceWriteDesktop');
            console.log("desktopInput")
        } else if (ysdk.deviceInfo.isMobile() || ysdk.deviceInfo.isTablet()) {
            myGameInstance.SendMessage('Progress', 'DeviceWriteMobile');
            console.log("mobileInput")
        }
    },

    SetToLeaderBoardDistance: function(value) {
        ysdk.getLeaderboards()
            .then(lb => {
                lb.setLeaderboardScore('MaxDistance', value);
            });
    },

    SetToLeaderBoardTotalCoins: function(value) {
        ysdk.getLeaderboards()
            .then(lb => {
                lb.setLeaderboardScore('TotalCoins', value);
            });
    },

    ShowRandomAd: function() {
        myGameInstance.SendMessage('Progress', 'TimeScaleZero');
        ysdk.adv.showFullscreenAdv({
            callbacks: {
                onClose: function(wasShown) {
                    myGameInstance.SendMessage('Progress', 'TimeScaleDefault');
                },
                onError: function(error) {
                    myGameInstance.SendMessage('Progress', 'TimeScaleDefault');
                }
            }
        })
    },

    StickyBannerOn: function() {
        ysdk.adv.getBannerAdvStatus().then(({ stickyAdvIsShowing, reason }) => {
            if (stickyAdvIsShowing) {
                // реклама показывается
            } else if (reason) {
                // реклама не показывается
                console.log(reason)
            } else {
                ysdk.adv.showBannerAdv()
            }
        })
    },

    StickyBannerOff: function() {
        ysdk.adv.getBannerAdvStatus().then(({ stickyAdvIsShowing, reason }) => {
            if (stickyAdvIsShowing) {
                ysdk.adv.hideBannerAdv()
            } else if (reason) {
                // реклама не показывается
                console.log(reason)
            } else {
                // ysdk.adv.showBannerAdv()
            }
        })
    },

    ShowAdForX2: function() {
        ysdk.adv.showRewardedVideo({
            callbacks: {
                onOpen: () => {
                    console.log('Video ad open.');
                    myGameInstance.SendMessage('Progress', 'TimeScaleZero');
                },
                onRewarded: () => {
                    myGameInstance.SendMessage('EventSystem', 'X2Apply');
                },
                onClose: () => {
                    console.log('Video ad closed.');
                    myGameInstance.SendMessage('Progress', 'TimeScaleDefault');
                },
                onError: (e) => {
                    console.log('Error while open video ad:', e);
                }
            }
        })
    },

    RateGameExt: function() {
        ysdk.feedback.canReview()
            .then(({ value, reason }) => {
                if (value) {
                    ysdk.feedback.requestReview()
                        .then(({ feedbackSent }) => {
                            console.log(feedbackSent);
                            myGameInstance.SendMessage('EventSystem', 'RateGameApply', feedbackSent);
                        })
                } else {
                    console.log(reason)
                    if (reason === "GAME_RATED") {
                        myGameInstance.SendMessage('EventSystem', 'RateGameApplyFixed', );
                    } else if (reason === "NO_AUTH") {
                        myGameInstance.SendMessage('EventSystem', 'RateGameApplyNOAUTH');
                    } else {
                        myGameInstance.SendMessage('EventSystem', 'RateGameApplySessionOff')
                    }
                }
            })
    },

    HelloStringExt: function(str) {
        window.alert(UTF8ToString(str));
    },

});